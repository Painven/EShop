<template>
    <div class="container">
        <div class="title-box">
            <h1>Список категорий магазина</h1>
            <a href="/category/add" class="btn btn-success px-3 py-1 m-2">
                <span class="emoji-icon">🆕</span>Добавить новую категорию</a
            >
        </div>

        <div v-if="categories.length == 0" class="loading-background-animation"></div>

        <div class="categories-list">
            <CategoryCard
                v-for="cat in categories"
                :id="cat.id"
                :title="cat.name"
                :imgsrc="cat.imageSmall"
                :productsInCategory="cat.productsInCategory"
            />
        </div>
    </div>
</template>

<script>
import CategoryRepository from "@/services/CategoryRepository.ts";
export default {
    data() {
        return {
            categories: [],
        };
    },
    async mounted() {
        this.categories = await CategoryRepository.getAllCategories();
    },
};
</script>
