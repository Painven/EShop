import { createRouter, createWebHistory, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/categories',
      name: 'categoriesList',
      component: () => import('../views/Category/CategoriesListView.vue')
    },
    {
      path: '/category/:id/products',
      name: 'categoryDetails',
      component: () => import('../views/Category/CategoryView.vue')
    },
    {
      path: '/category/:id/edit',
      name: 'categoryEdit',
      component: () => import('../views/Category/CategoryEditView.vue')
    },
    {
      path: '/category/add',
      name: 'categoryAdd',
      component: () => import('../views/Category/CategoryAddView.vue')
    },
    {
      path: '/product/add',
      name: 'productAdd',
      component: () => import('../views/Product/ProductAddView.vue')
    },
    {
      path: '/product/:id/edit',
      name: 'productEdit',
      component: () => import('../views/Product/ProductEditView.vue')
    },
    {
      path: '/checkout',
      name: 'checkout',
      component: () => import('../views/CheckoutView.vue')
    },
    {
      path: '/orders/:id',
      name: 'orderDetails',
      component: () => import('../views/Order/OrderDetails.vue')
    },
    {
      path: '/orders',
      name: 'orders',
      component: () => import('../views/Order/OrdersView.vue')
    },
    {
      path: '/search',
      name: 'search',
      component: () => import('../views/SearchView.vue')
    }
  ]
})

export default router
