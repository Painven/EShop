<template>
    <div class="container">
        <div class="category-details-container">
            <div class="title-box">
                <h1 class="text-center">
                    {{ categoryInfo.name }}
                </h1>
                <a href="/product/add" class="btn btn-success px-3 py-1 m-2">
                    <span class="emoji-icon">🆕</span>Добавить новый товар</a
                >
            </div>
            <CategoryDetailsPaginator
                :categoryId="categoryId"
                :currentPage="currentPage"
                :productsPerPage="pageSize"
                :totalProducts="this.categoryInfo.productsInCategory"
            ></CategoryDetailsPaginator>
            <hr />
            <div class="products-list">
                <ProductCard
                    v-for="product in categoryInfo.products"
                    :id="product.id"
                    :productName="product.name"
                    :price="product.price"
                    :imgsrc="product.imageSmall"
                />
            </div>
        </div>

        <CategoryDetailsPaginator
            :categoryId="categoryId"
            :currentPage="currentPage"
            :productsPerPage="pageSize"
            :totalProducts="this.categoryInfo.productsInCategory"
        ></CategoryDetailsPaginator>
    </div>
</template>

<script>
import CategoryRepository from "@/services/CategoryRepository.ts";
export default {
    data() {
        return {
            pageSize: 4 * 5, // 5 строк по 4 товара = 20
            categoryId: 0,
            currentPage: 1,
            categoryInfo: {},
        };
    },
    async mounted() {
        this.categoryId = this.$route.params.id;
        this.currentPage = Number.parseInt(this.$route.query.page) || 1;

        this.categoryInfo = await CategoryRepository.getCategoryById(
            this.categoryId,
            this.currentPage,
            this.pageSize
        );
    },
};
</script>
