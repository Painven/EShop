

<template>
  <div class="container">
    <div class="category_h1_wrapper">
    <h1>{{ category.name }}</h1>
  </div>

  <form class="category-edit-form" method="post" @submit="sendForm">
    <div class="form-group">
      <label for="categoryName">Название категории</label>
      <input
        v-model="category.name"
        name="name"
        type="text"
        class="form-control"
        id="categoryName"
        placeholder="Введите название"
      />
    </div>
    <div class="form-group">
      <label for="categoryImage">URL изображения</label>
      <input
        v-model="category.image"
        name="image"
        type="url"
        class="form-control"
        id="categoryImage"
        placeholder="https://"
      />
    </div>
    <div v-if="category.image" class="image-fluid">
      <img :src="category.image" class="w-100 h-100" />
    </div>
    <hr />

    <button type="submit" class="btn btn-lg btn-success">Создать</button>
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
import CategoryRepository from "@/services/CategoryRepository.ts";

export default {
  methods: {
     sendForm: async function (e) {
      e.preventDefault();
      this.errors = [];

      if (this.category.name && this.category.image) {
        
        const apiResult = await CategoryRepository.addCategory(this.category);
        if(apiResult){
            this.$router.push('/categories');
          }else{
            this.errors.push("Ошибка создания категории");
          }
          
      }else{
        let focusChanged = false;
          if (!this.category.name) {
              this.errors.push("Необходимо заполнить название");
              this.$el.querySelector('#categoryName').focus();   
              focusChanged = true;
          }
          if (!this.category.image) {
              this.errors.push("Необходимо указать изображение");
              if(!focusChanged){
                this.$el.querySelector('#categoryImage').focus();  
                focusChanged = true;
              }
          }
      }
    }
  },
  data() {
    return {
      category: {
        name: '',
        image: ''
      },
      errors: []
    };
  }
};

</script>
