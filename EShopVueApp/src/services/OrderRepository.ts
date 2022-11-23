import type { IOrderModel, IOrderStatus } from "@/models/order";
import EShopApi from "@/services/EShopApi";

export default {
    async getOrderById(orderId: number): Promise<IOrderModel | null> {
        return await EShopApi.SendRequest(`orders/${orderId}`);
    },
    async changeStatus(orderId: number, newStatus: string) {
        return await EShopApi.SendRequest(`orders/${orderId}?newStatus=${newStatus}`, "PATCH");
    },
    async getAll(): Promise<IOrderModel[] | null> {
        return await EShopApi.SendRequest('orders');
    },
    async getStatuses(): Promise<IOrderStatus[] | null> {
        return await EShopApi.SendRequest('orders/statuses');
    },
    async makeOrder(orderData: IOrderModel): Promise<IOrderModel | null> {
        return await EShopApi.SendRequest('orders', 'POST', orderData);
    }
}