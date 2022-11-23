<template>
  <div class="container">
    <div class="product_h1_wrapper">
      <h1>{{ newProduct.name || '<Без названия>' }}</h1>
    </div>

  <form class="product-add-form" @submit="sendForm">
    <div class="form-group">
      <label for="productName">Название товара</label>
      <input
        v-model="newProduct.name"
        name="name"
        type="text"
        class="form-control"
        id="productName"
      />
    </div>
    <div class="row">
      <div class="col">
        <div class="form-group">
          <label for="productName">Категория</label>
          <select v-model="selectedCategory" class="form-control">
            <option v-if="selectedCategory == 0" disabled value="">Выбрать категорию</option>
            <option v-for="cat in categories" 
              :selected="cat.id == selectedCategory" 
              :value="cat.id" :key="cat.name" >{{cat.name}}</option>
          </select>
      </div>
      </div>
<div class="col">
  <div class="form-group">
      <label for="productName">Цена товара</label>
      <input
        v-model="newProduct.price"
        name="price"
        type="number"
        min="0" max="999999" 
        class="form-control"
        id="productName"
      />
    </div>
</div>
    </div>
    

    <div class="form-group">
      <label for="productImage">URL изображения</label>
      <input
        v-model="newProduct.image"
        name="image"
        type="url"
        class="form-control"
        id="productImage"
        placeholder="https://"
      />
    </div>

    <div v-if="newProduct.image" class="image-fluid" >
      <img :src="newProduct.image" class="w-100 h-100" />
    </div>

    <hr />

    <button type="submit" class="btn btn-lg btn-success">Добавить</button>

    <p v-if="errors.length">
      <b>Нужно исправить ошибки</b>
      <ul class="form-error-list">
        <li v-for="error in errors" class="bg-danger">{{ error }}</li>
      </ul>
    </p>

  </form>
  </div>

</template>

<script>
import ProductRepository from "@/services/ProductRepository.ts";
import CategoryRepository from "@/services/CategoryRepository.ts";

export default {
  data() {
    return {
      categories: [],
      selectedCategory: '',
      newProduct: {},
      errors: [],
    };
  },
  methods: {
     sendForm: async function (e) {
        e.preventDefault();
        this.errors = [];

        if (this.newProduct.name && this.newProduct.image && this.selectedCategory) {  
          
          const apiResult = await ProductRepository.addProduct(this.newProduct, parseInt(this.selectedCategory));

          if(apiResult){
            this.$router.push(`/category/${this.selectedCategory}/products`);
          }else{
            this.errors.push("Ошибка создания товара");
          }
        }
        else
        {
          let focusChanged = false;
          if (!this.newProduct.name) {
              this.errors.push("Необходимо заполнить название");
              this.$el.querySelector('#productName').focus();   
              focusChanged = true;
          }
          if (!this.newProduct.image) {
              this.errors.push("Необходимо указать изображение");
              if(!focusChanged){
                this.$el.querySelector('#productImage').focus();  
                focusChanged = true;
              }
          }
          if (!this.selectedCategory.id) {
              this.errors.push("Необходимо выбрать категорию товара");   
              if(!focusChanged){ 
                this.$el.querySelector('#productCategory').focus();   
                focusChanged = true;
            }
          } 
        }
    },
  },
  async mounted() {
    this.categories = await CategoryRepository.getAllCategories();
    const categoryId = document.referrer.match(/([0-9]+)\/products/)[1];
    this.selectedCategory = categoryId;
  },
};
</script>
