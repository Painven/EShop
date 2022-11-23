<template>
  <div v-if="totalProducts > productsPerPage" class="paginator">
    <nav>
      <ul class="pagination pagination-lg">
        <li
          v-for="paginatorPage in pagesArray"
          :key="paginatorPage"
          :class="{ 'page-item': true, disabled: currentPage == paginatorPage }"
        >
          <router-link
            class="page-link"
            :to="{
              name: 'categoryDetails',
              params: { id: categoryId },
              query: { page: paginatorPage },
              force: true,
            }"
          >
            {{ paginatorPage }}
          </router-link>
        </li>
      </ul>
    </nav>
    <p class="paginator-info">{{ productNumbersInfo }}</p>
  </div>
</template>

<script>
export default {
  props: ["categoryId", "currentPage", "productsPerPage", "totalProducts"],
  mounted() {},
  computed: {
    productNumbersInfo() {
      const start = 1 + (this.currentPage - 1) * this.productsPerPage;
      const end = Math.min(start + this.productsPerPage - 1, this.totalProducts);
      return `Товары с ${start} по ${end}`;
    },
    pagesArray() {
      const pages = [];
      const maxPages = 15;
      const pagesSideCount = Math.floor(maxPages / 2);
      const totalPages = Math.ceil(this.totalProducts / this.productsPerPage) + 1;

      const startPage = Math.max(this.currentPage - pagesSideCount, 1);
      const endPage = Math.min(this.currentPage + pagesSideCount, totalPages);

      //   console.log("productsPerPage:" + this.productsPerPage);
      //   console.log("totalProducts:" + this.totalProducts);
      //   console.log("totalPages:" + totalPages);
      //   console.log("currentPage:" + this.currentPage);
      //   console.log("maxPages:" + maxPages);
      //   console.log("start page:" + startPage);
      //   console.log("end page: " + endPage);

      for (let i = startPage; i < endPage; i++) {
        pages.push(i);
        if (pages.length > maxPages) {
          break;
        }
      }
      return pages;
    },
  },
};
</script>
