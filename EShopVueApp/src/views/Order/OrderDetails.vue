<template>
    <div class="container">
        <h1 class="m-2">
            Детали заказа № <strong>{{ orderId }}</strong> на сумму
            {{ orderInfo.totalSum }} руб.
        </h1>

        <div class="order-checkout-container">
            <div class="table-responsive">
                <table class="table table-bordered table-dark order-table">
                    <thead>
                        <th>Товар</th>
                        <th width="1%">Стоимость</th>
                        <th width="1%">Кол-во</th>
                        <th width="1%">Сумма</th>
                    </thead>
                    <tbody>
                        <tr v-for="p in orderInfo.products">
                            <td>{{ p.productName }}</td>
                            <td class="price-cell">{{ ceilPrice(p.price) }} руб.</td>
                            <td>
                                <div class="quantity-cell">
                                    <span>{{ p.quantity }}</span>
                                </div>
                            </td>
                            <td>{{ p.quantity * ceilPrice(p.price) }} руб.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="order-details-summary">
            <div class="order-details-summary-section">
                <h3>Заказчик</h3>
                <p>
                    Имя: <strong>{{ orderInfo.customerName }}</strong>
                </p>
                <p>
                    Email: <strong>{{ orderInfo.customerEmail }}</strong>
                </p>
            </div>
            <div class="order-details-summary-section">
                <h3>Детали</h3>
                <p>
                    Способ доставки: <strong>{{ orderInfo.shippingMethod }}</strong>
                </p>
                <p>
                    Способ оплаты: <strong>{{ orderInfo.paymentMethod }}</strong>
                </p>
            </div>
            <div class="order-details-summary-section">
                <h3>Статус '{{ orderInfo.orderStatus }}'</h3>
                <p>
                    Дата создания:
                    <strong>{{ $filters.dateFormatter(orderInfo.created) }}</strong>
                </p>
                <p>
                    Дата завершения:
                    <strong>{{ $filters.dateFormatter(orderInfo.completeDate) }}</strong>
                </p>
            </div>
        </div>
        <button
            class="btn btn-lg btn-primary w-100"
            @click="notifyCustomerShipping"
            :disabled="isCustomerNotified || orderInfo.orderStatus != 'Выполнен'"
        >
            Оповестить клиента о доставке заказа
        </button>
    </div>
</template>

<script>
import OrderRepository from "@/services/OrderRepository.ts";

export default {
    data() {
        return {
            orderId: 0,
            isCustomerNotified: false,
            orderInfo: {},
        };
    },
    methods: {
        ceilPrice(price) {
            return Math.floor(parseInt(price));
        },
        notifyCustomerShipping() {
            this.isCustomerNotified = true;
            alert("Не реализовано");
        },
    },
    async mounted() {
        this.orderId = this.$route.params.id;
        if (this.orderId !== 0) {
            this.orderInfo = await OrderRepository.getOrderById(this.orderId);
        }
    },
};
</script>
