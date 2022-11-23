import IProduct from './product'

export interface ICategoryModel {
    image: string
    imageSmall: string
    name: string
    id: number
    productsInCategory: number
    products: IProduct[]
}

