<template>
  <div class="container">
    <div class="product_h1_wrapper">
      <h1>{{ editProduct.name || '<Без названия>' }}</h1>
    </div>

  <form class="product-add-form" @submit="sendForm">
    <div class="form-group">
      <label for="productName">Название товара</label>
      <input
        v-model="editProduct.name"
        name="name"
        type="text"
        class="form-control"
        id="productName"
      />
    </div>
    <div class="row">
      <div class="col">
        <div class="form-group">
          <label for="productCategory">Категория</label>
          <select v-model="editProduct.categoryId" class="form-control" id="productCategory">
            <option v-if="editProduct.categoryId == 0" disabled value="">Выбрать категорию</option>
            <option v-for="cat in categories" 
              :selected="cat.id == editProduct.categoryId" 
              :value="cat.id" :key="cat.name" >{{cat.name}}</option>
          </select>
      </div>
      </div>
<div class="col">
  <div class="form-group">
      <label for="productPrice">Цена товара</label>
      <input
        v-model="editProduct.price"
        name="price"
        type="number"
        min="0" max="999999" 
        class="form-control"
        id="productPrice"
      />
    </div>
</div>
    </div>
    

    <div class="form-group">
      <label for="productImage">URL изображения</label>
      <input
        v-model="editProduct.image"
        name="image"
        type="url"
        class="form-control"
        id="productImage"
        placeholder="https://"
      />
    </div>

    <div v-if="editProduct.image" class="image-fluid" >
      <img :src="editProduct.image" class="w-100 h-100" />
    </div>

    <hr />

    <button type="submit" class="btn btn-lg btn-success" :disabled="editButtonDisabled">Обновить товар</button>

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
      editProduct: {},
      originalProduct: {},
      errors: [],
    };
  },
  methods: {
     sendForm: async function (e) {
        e.preventDefault();
        this.errors = [];

        if (this.editProduct.name && this.editProduct.image && this.editProduct.categoryId) {  
          
          const apiResult = await ProductRepository.editProduct(this.editProduct);

          if(apiResult){
            this.$router.push(`/category/${this.editProduct.categoryId}/products`);
          }else{
            this.errors.push("Ошибка изменения товара");
          }
        }
        else
        {
          let focusChanged = false;
          if (!this.editProduct.name) {
              this.errors.push("Необходимо заполнить название");
              this.$el.querySelector('#productName').focus();   
              focusChanged = true;
          }
          if (!this.editProduct.image) {
              this.errors.push("Необходимо указать изображение");
              if(!focusChanged){
                this.$el.querySelector('#productImage').focus();  
                focusChanged = true;
              }
          }
          if (!this.editProduct.categoryId) {
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
    this.editProduct = await ProductRepository.getProductById(this.$route.params.id);
    Object.assign(this.originalProduct, this.editProduct);

    this.categories = await CategoryRepository.getAllCategories();
  },
  computed: {
    editButtonDisabled: function(){
      return JSON.stringify(this.originalProduct) == JSON.stringify(this.editProduct);
    }
  }
};
</script>
