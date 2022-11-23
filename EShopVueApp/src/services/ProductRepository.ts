import type { IProductModel } from "@/models/product";
import EShopApi from "@/services/EShopApi";

export default {
   async addProduct(product: IProductModel, categoryId: number): Promise<boolean> {
      return await EShopApi.SendRequest('products', 'POST', { ...product, categoryId });
   },
   async editProduct(product: IProductModel): Promise<boolean> {
      return await EShopApi.SendRequest('products', 'PUT', product);
   },
   async getProductById(productId: number) {
      return await EShopApi.SendRequest(`products/${productId}`);
   },
   async deleteProductById(productId: number): Promise<boolean> {
      return await EShopApi.SendRequest(`products/${productId}`, 'DELETE');
   },
   async findProducts(text: string): Promise<IProductModel[] | null> {
      return await EShopApi.SendRequest(`products/search/${text}`);
   },
   async getBestsellers(itemsCount: number): Promise<IProductModel[] | null> {
      return await EShopApi.SendRequest(`products/bestsellers?itemsCount=${itemsCount}`);
   }
}