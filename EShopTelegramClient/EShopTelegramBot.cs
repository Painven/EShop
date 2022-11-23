using EShopTelegramClient;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class EShopTelegramBot
{
    private readonly ITelegramBotClient botClient;
    private readonly IOrderStatusRepository ordersStatusRepo;
    private readonly IReadOnlyDictionary<string, string> callbacksData = new Dictionary<string, string>()
    {
        ["/check_order"] = "Проверить статус заказа",
        ["/start"] = "В начало",
    };
    private readonly Dictionary<long, string> activeDialogs = new();

    public EShopTelegramBot(ITelegramBotClient botClient, IOrderStatusRepository ordersStatusRepo)
    {
        this.botClient = botClient;
        this.ordersStatusRepo = ordersStatusRepo;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message)
        {       
            if (!activeDialogs.ContainsKey(update.Message.Chat.Id))
            {
                //Новый диалог
                await HandleMessages(update.Message);
            }
            else
            {             
                // Продолжаем диалог уже с какого-то месте
                await HandleActiveDialog(update.Message);
            }
        }
        if (update.Type == UpdateType.CallbackQuery)
        {
            await HandleCallbackQuery(update);
        }
    }

    private async Task HandleActiveDialog(Message message)
    {
        Dictionary<string, string> ordersInfo = ordersStatusRepo.GetOrderStatuses();

        string data = ordersInfo.ContainsKey(message.Text) ? ordersInfo[message.Text] : null;

        string msg = (data != null) ?
            $"Статус вашего заказа: {data}" :
            $"Заказ с номером '{message.Text}' не найден!";

        await botClient.SendTextMessageAsync(message.Chat, msg);
        activeDialogs.Remove(message.Chat.Id);
    }

    private async Task HandleMessages(Message message)
    {
        string requestText = message.Text.ToLower();

        if (requestText == "Получить заказ")
        {
            await this.botClient.SendTextMessageAsync(message.Chat, "Ваш заказ");
        }

        if (requestText == "/start")
        {
            var msg = "Бот может выполнить следующий действия";
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: callbacksData["/check_order"], callbackData: "/check_order"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: callbacksData["/start"], callbackData: "/start"),
                },

            });

            await botClient.SendTextMessageAsync(message.Chat, msg, replyMarkup: inlineKeyboard);
        }
    }

    private async Task HandleCallbackQuery(Update update)
    {
        string codeOfButton = update.CallbackQuery.Data;

        if (codeOfButton == "/check_order")
        {
            await botClient.SendTextMessageAsync(
                chatId: 
                update.CallbackQuery.Message.Chat.Id, 
                "Укажите номер вашего заказа");

            activeDialogs[update.CallbackQuery.Message.Chat.Id] = "/check_order";
        }
    }

    private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        // Некоторые действия
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(this.botClient));
    }

    public void Start()
    {
        LogMessage("Запуск бота " + botClient.GetMeAsync().Result.FirstName);

        try
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            botClient.StartReceiving(HandleUpdateAsync,
                    HandleErrorAsync,
                    receiverOptions,
                    cancellationToken
                );
            LogMessage("Бот запущен!");

        }
        catch(Exception ex)
        {
            LogMessage("Ошибка запуска: " + ex.Message);
        }
    }

    private void LogMessage(string message)
    {
        Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}] {message}");
    }
}

