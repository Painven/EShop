import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import ProductCard from './components/ProductCard.vue'
import HeaderNavigationMenu from './components/HeaderNavigationMenu.vue'
import SearchBox from './components/SearchBox.vue'
import CategoryCard from './components/CategoryCard.vue'
import CategoryDetailsPaginator from './components/CategoryDetailsPaginator.vue'
import HeaderMenu from './components/HeaderMenu.vue'
import FooterMenu from './components/FooterMenu.vue'
import CartMenu from './components/CartMenu.vue'
import OrderSuccessDialog from './components/OrderSuccessDialog.vue'
import OrderStatusDropdownButton from './components/OrderStatusDropdownButton.vue'
import ProductsCarousel from './components/ProductsCarousel.vue'

import './assets/main.css'

const app = createApp(App)

app.component("ProductCard", ProductCard);
app.component("CategoryCard", CategoryCard);
app.component("CategoryDetailsPaginator", CategoryDetailsPaginator);
app.component("HeaderMenu", HeaderMenu);
app.component("FooterMenu", FooterMenu);
app.component("CartMenu", CartMenu);
app.component("OrderSuccessDialog", OrderSuccessDialog);
app.component("OrderStatusDropdownButton", OrderStatusDropdownButton);
app.component("HeaderNavigationMenu", HeaderNavigationMenu);
app.component("SearchBox", SearchBox);
app.component("ProductsCarousel", ProductsCarousel);
app.use(router);
app.mount('#app')


app.config.globalProperties.$filters = {
    dateFormatter(value: string) {
        if (value) {
            return new Intl.DateTimeFormat().format(new Date(value));
        }
        return '-';
    }
}