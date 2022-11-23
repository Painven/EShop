<template>
    <div class="header-cart-container">
        <div class="cart-menu-container">
            <div class="cart-menu-inner">
                <div class="cart-menu-icon">
                    <router-link to="/checkout">
                        <span class="emoji-icon">🛒</span>
                    </router-link>
                </div>
                <div v-if="productsInCart" class="cart-menu-count">
                    {{ productsInCart }} товара(ов)
                </div>
                <div v-if="productsInCart" class="cart-menu-price">
                    {{ totalSum }} руб.
                </div>
                <div
                    v-if="productsInCart"
                    class="cart-menu-icon"
                    @click="showDetails = !showDetails"
                >
                    <span class="emoji-icon">📇</span>
                </div>
                <div v-if="canShowCartDetails" class="cart-dropdown-menu">
                    <button class="btn btn-sm bg-primary" @click="clearCart">
                        Очистить
                    </button>
                    <a v-for="p in cart.products" class="dropdown-item" href="#">
                        {{ p.quantity }}шт. {{ p.name }}
                        <span style="color: red">({{ p.price }}</span> руб.)
                    </a>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            cart: {},
            showDetails: false,
        };
    },
    computed: {
        canShowCartDetails: function () {
            return this.showDetails && this.cart;
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
    methods: {
        clearCart() {
            clearCartProducts();
            this.showDetails = false;
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
