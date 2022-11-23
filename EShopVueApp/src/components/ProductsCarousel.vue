<template>
    <div class="products-list">
        <ProductCard
            v-for="product in products"
            :key="product.id"
            :id="product.id"
            :productName="product.name"
            :price="product.price"
            :imgsrc="product.imageSmall"
        />
    </div>
</template>

<script>
import ProductRepository from "@/services/ProductRepository";
export default {
    data() {
        return {
            products: [],
        };
    },
    async mounted() {
        this.products = await ProductRepository.getBestsellers(4);
    },
};
</script>
