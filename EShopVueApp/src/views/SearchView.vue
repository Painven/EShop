<template>
    <div class="container">
        <div class="category-details-container">
            <h1 class="text-center pt-4">
                Поиск товаров - найдено {{ foundedProducts.length }}
            </h1>
            <hr />
            <div class="products-list">
                <ProductCard
                    v-for="product in foundedProducts"
                    :id="product.id"
                    :productName="product.name"
                    :price="product.price"
                    :imgsrc="product.imageSmall"
                />
            </div>
        </div>
    </div>
</template>

<script>
import ProductRepository from "@/services/ProductRepository.ts";

export default {
    data() {
        return {
            foundedProducts: [],
        };
    },
    async mounted() {
        const searchText = this.$route.query.text;
        if (searchText.length > 3) {
            this.foundedProducts = await ProductRepository.findProducts(searchText);
        }
    },
};
</script>
