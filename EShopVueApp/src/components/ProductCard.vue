<template>
    <div class="card product-item">
        <img class="card-img-top" :src.once="imgsrc" :alt="productName" />
        <div class="card-body">
            <h5 class="product-name">{{ productName }}</h5>
            <p class="card-text price">
                Цена: <span class="text-bold">{{ price }} руб.</span>
            </p>
            <div class="card-buttons">
                <router-link
                    :to="{
                        name: 'productEdit',
                        params: { id: id },
                    }"
                    class="btn btn-secondary"
                >
                    <span class="emoji-icon">✏️</span>
                </router-link>
                <button class="btn btn-sm btn-primary" @click="addToCart">
                    <span class="emoji-icon">🛒</span>
                </button>
                <button
                    class="btn btn-secondary btn-danger btn-sm"
                    @click="showDeleteProductDialog"
                >
                    <span class="emoji-icon emoji-icon-small">🗑️</span>
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import ProductRepository from "@/services/ProductRepository.ts";

export default {
    props: ["id", "productName", "imgsrc", "price"],
    methods: {
        addToCart() {
            const product = {
                id: this.id,
                name: this.productName,
                price: this.price,
            };
            addProductToCart(product, 1);
        },
        async showDeleteProductDialog() {
            const confirmResult = confirm(`Удалить товар '${this.productName}'?`);
            if (confirmResult) {
                await ProductRepository.deleteProductById(this.id);
                this.$router.push({
                    name: "categoryDetails",
                    params: { id: this.$route.params.id },
                });
            }
        },
    },
};
</script>
