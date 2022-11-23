<template>
    <div class="container">
        <h1>Оформление заказа</h1>

        <div v-if="successOrderInfo.id">
            <OrderSuccessDialog
                :customerName="successOrderInfo.customerName"
                :orderId="successOrderInfo.id"
            />
        </div>
        <div v-if="cart == null || cart.products == null || cart.products.length == 0">
            <h2>Корзина пуста!</h2>
        </div>

        <div v-else class="order-checkout-container">
            <div class="table-responsive">
                <table class="table table-bordered table-dark order-table">
                    <thead>
                        <th>Товар</th>
                        <th width="1%">Стоимость</th>
                        <th width="1%">Количество</th>
                        <th width="1%">Убрать</th>
                    </thead>
                    <tbody>
                        <tr v-for="p in cart.products">
                            <td>{{ p.name }}</td>
                            <td class="price-cell">{{ p.price }} руб.</td>
                            <td>
                                <div class="quantity-cell">
                                    <button
                                        :disabled="p.quantity == 1"
                                        @click="changeQuantity(p, -1)"
                                        class="btn btn-sm btn-outline-info"
                                    >
                                        -
                                    </button>
                                    <span>{{ p.quantity }}</span>
                                    <button
                                        @click="changeQuantity(p, +1)"
                                        class="btn btn-sm btn-outline-info"
                                    >
                                        +
                                    </button>
                                </div>
                            </td>
                            <td>
                                <button
                                    @click="removeProduct(p)"
                                    class="btn btn-sm btn-danger w-100"
                                >
                                    -
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div v-if="!isConfirmed" class="order-bottom-panel">
                <p style="font-size: 32px; color: yellow">ИТОГО: {{ totalSum }} руб.</p>
                <button
                    @click="isConfirmed = true"
                    class="btn btn-lg btn-success px-4 m-2"
                >
                    Подтвердить
                </button>
            </div>
        </div>

        <div v-if="isConfirmed">
            <div class="order-checkout-confirm">
                <div class="order-checkout-section">
                    <h2>Контактные данные</h2>

                    <div class="form-group">
                        <label class="form-label" for="customer-name">Ваше имя</label>
                        <input
                            v-model="customerInfo.name"
                            type="text"
                            class="form-control"
                            id="customer-name"
                        />
                    </div>

                    <div class="form-group">
                        <label class="form-label" for="customer-email">Ваш email</label>
                        <input
                            v-model="customerInfo.email"
                            type="text"
                            class="form-control"
                            id="customer-email"
                        />
                    </div>
                </div>

                <div class="order-checkout-section shipping-section">
                    <h2>Доставка</h2>
                    <div v-for="(sm, ind) in shippingMethods" class="form-check">
                        <input
                            class="form-check-input"
                            type="radio"
                            name="shippingMethodGroup"
                            :id="'shippingMethod' + ind"
                            :value="sm"
                            v-model="selectedShippingMethod"
                        />
                        <label class="form-check-label" :for="'shippingMethod' + ind">
                            {{ sm }}
                        </label>
                    </div>
                </div>

                <div class="order-checkout-section payment-section">
                    <h2>Оплата</h2>
                    <div v-for="(pm, ind) in paymentMethods" class="form-check">
                        <input
                            class="form-check-input"
                            type="radio"
                            name="paymentMethodGroup"
                            :id="'paymentMethod' + ind"
                            :value="pm"
                            v-model="selectedPaymentMethod"
                        />
                        <label class="form-check-label" :for="'paymentMethod' + ind">
                            {{ pm }}
                        </label>
                    </div>
                </div>
            </div>
            <button
                @click="submitOrder"
                :disabled="sendBtnDisabled"
                class="btn btn-lg btn-primary w-100"
            >
                <span
                    v-if="isSendInProgress"
                    class="spinner-grow spinner-grow-lg text-warning"
                    style="margin-right: 20px"
                    role="status"
                    aria-hidden="true"
                ></span>
                <span class="sr-only">Отправить</span>
            </button>
        </div>
    </div>
</template>

<script>
import OrderRepository from "@/services/OrderRepository.ts";

export default {
    data() {
        return {
            cart: {},
            paymentMethods: ["Наличными", "Картой онлайн", "Банковский перевод"],
            selectedPaymentMethod: "Наличными",
            shippingMethods: ["Самовывоз", "Курьером", "Транспортной компанией"],
            selectedShippingMethod: "Самовывоз",
            isConfirmed: false,
            isSendInProgress: false,
            customerInfo: {
                name: "",
                email: "",
            },
            successOrderInfo: {},
        };
    },
    methods: {
        changeQuantity(product, changeDirection) {
            addProductToCart(product, changeDirection);
        },
        removeProduct(product) {
            removeProductFromCart(product);
        },
        async submitOrder() {
            this.isSendInProgress = true;

            const cartProducts = this.cart.products.map(function (product) {
                return {
                    productId: product.id,
                    quantity: product.quantity,
                };
            });

            const data = {
                customerName: this.customerInfo.name,
                customerEmail: this.customerInfo.email,
                paymentMethod: this.selectedPaymentMethod,
                shippingMethod: this.selectedShippingMethod,
                products: cartProducts,
            };

            this.successOrderInfo = await OrderRepository.makeOrder(data);
            this.isSendInProgress = false;

            if (this.successOrderInfo != null) {
                clearCartProducts();
                setTimeout(() => {
                    this.$router.push("/");
                }, 3000);
            }
        },
    },
    computed: {
        sendBtnDisabled: function () {
            return (
                Object.keys(this.successOrderInfo).length > 0 ||
                !this.customerInfo.name ||
                !this.customerInfo.email
            );
        },
        productsInCart: function () {
            if (this.cart && this.cart.products) {
                return this.cart.products.length || 0;
            }
        },
        totalSum: function () {
            if (this.cart && this.cart.products) {
                return this.cart.products.reduce((sum, product) => {
                    return sum + product.price * product.quantity;
                }, 0);
            }
        },
    },
    mounted() {
        this.cart = JSON.parse(localStorage.getItem("cart"));
        addEventListener("cart-localstorage-changed", (event) => {
            this.cart = JSON.parse(localStorage.getItem("cart"));
        });
    },
};
</script>
