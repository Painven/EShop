export interface IOrderLineModel {
    productId: number
    quantity: number
}
export interface IOrderModel {
    id: number
    created: string
    customerName: string
    customerEmail: string
    paymentMethod: string
    shippingMethod: string
    totalSum: number | undefined
    orderStatus: string
    completeDate: string | undefined | null
    products: IOrderLineModel[]
}

export interface IOrderStatus {
    id: number
    name: string
    sort_order: number
}