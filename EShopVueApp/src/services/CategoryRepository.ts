import type { ICategoryModel } from "@/models/category";
import EShopApi from "@/services/EShopApi";

export default {
   async getAllCategories(): Promise<ICategoryModel[]> {
      return await EShopApi.SendRequest('categories');
   },
   async getCategoryById(catId: number, page: number, pageSize: number): Promise<ICategoryModel> {
      return await EShopApi.SendRequest(`categories/${catId}?page=${page || 8}&pageSize=${pageSize || 32}`);
   },
   async updateCategory(category: ICategoryModel): Promise<boolean> {
      return await EShopApi.SendRequest('categories', 'PUT', category);
   },
   async deleteCategory(categoryId: number) {
      return await EShopApi.SendRequest(`categories/${categoryId}`, 'DELETE');
   },
   async addCategory(category: ICategoryModel): Promise<boolean> {
      return await EShopApi.SendRequest(`categories}`, 'POST', category);
   }
}