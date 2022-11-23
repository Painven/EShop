<template>
    <div class="header-search-container">
        <input
            type="text"
            id="header-search"
            v-on:keyup.enter="onSearchEnterPress"
            v-model="searchText"
            class="form-control"
            placeholder="Поиск"
        />
    </div>
</template>

<script>
export default {
    data() {
        return {
            searchText: "",
        };
    },
    methods: {
        onSearchEnterPress() {
            if (this.searchText.length > 3) {
                this.$router.push("/search/?text=" + encodeURI(this.searchText));
            }
        },
    },
};
</script>
