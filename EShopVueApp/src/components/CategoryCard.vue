<template>
    <div class="card category-item">
        <img class="card-img-top" :src.once="imgsrc" :alt="productsInCategory" />
        <div class="card-body">
            <h5 class="card-title">{{ title }}</h5>
            <p class="card-text">{{ productsInCategory }} товара(ов)</p>

            <div class="category-links">
                <router-link
                    class="btn btn-primary"
                    :to="{
                        name: 'categoryDetails',
                        params: {
                            id: id,
                        },
                    }"
                >
                    Товары
                </router-link>
                <router-link
                    class="btn btn-secondary btn-sm"
                    :to="{
                        name: 'categoryEdit',
                        params: {
                            id: id,
                        },
                    }"
                >
                    <span class="emoji-icon emoji-icon-small">✏️</span>
                </router-link>
                <button
                    class="btn btn-secondary btn-danger btn-sm"
                    @click="showDeleteCategoryDialog"
                >
                    <span class="emoji-icon emoji-icon-small">🗑️</span>
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import CategoryRepository from "@/services/CategoryRepository.ts";

export default {
    props: ["id", "title", "imgsrc", "productsInCategory"],
    methods: {
        async showDeleteCategoryDialog() {
            const confirmResult = confirm(`Удалить категорию '${this.title}'?`);
            if (confirmResult) {
                await CategoryRepository.deleteCategory(this.id);
                this.$router.push({ name: "categoriesList" });
            }
        },
    },
};
</script>
