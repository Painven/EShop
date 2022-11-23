<template>
    <div class="container">
        <h1>Заказы</h1>
        <div v-if="orders.length == 0" class="loading-background-animation"></div>

        <div class="orders-list-top-panel">
            <p>
                Активных заказов: <strong>{{ activeOrdersCount }}</strong>
            </p>
            <div class="form-check">
                <input
                    class="form-check-input"
                    type="checkbox"
                    v-model="showOnlyActiveOrders"
                    id="flexCheckDefault"
                />
                <label class="form-check-label" for="flexCheckDefault">
                    Скрыть завершенные заказы
                </label>
            </div>
        </div>
        <div class="table-responsive">
            <table class="table table-bordered table-dark order-table">
                <thead>
                    <th width="1%">№</th>
                    <th width="1%">Дата</th>
                    <th width="1%">Сумма</th>
                    <th>Закачик</th>
                    <th v-if="!showOnlyActiveOrders" width="1%">Завершен</th>
                    <th width="1%">Статус</th>
                    <th width="1%">Детали</th>
                </thead>
                <tbody>
                    <tr v-for="o in filteredOrders" :key="o.id">
                        <td>{{ o.id }}</td>
                        <td>{{ $filters.dateFormatter(o.created) }}</td>
                        <td>{{ o.totalSum }} руб.</td>
                        <td>{{ o.customerName }}</td>
                        <td v-if="!showOnlyActiveOrders">
                            {{ $filters.dateFormatter(o.completeDate) }}
                        </td>
                        <td class="order-status-cell">
                            <OrderStatusDropdownButton
                                :orderStatus="o.orderStatus"
                                :orderId="o.id"
                                :orderStatusesSource="orderStatuses"
                            />
                        </td>
                        <td class="order-status-cell">
                            <router-link
                                class="btn btn-sm btn-secondary"
                                :to="{
                                    name: 'orderDetails',
                                    params: {
                                        id: o.id,
                                    },
                                }"
                            >
                                👁️
                            </router-link>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>
<script>
import OrderRepository from "@/services/OrderRepository.ts";

export default {
    data() {
        return {
            showOnlyActiveOrders: false,
            orders: [],
            orderStatuses: [],
        };
    },
    computed: {
        activeOrdersCount: function () {
            return this.orders.filter((order) => !order.completeDate).length;
        },
        filteredOrders: function () {
            let source = this.showOnlyActiveOrders
                ? this.orders.filter((order) => !order.completeDate)
                : this.orders;

            const filteredSource = source.sort((o1, o2) => o2.id - o1.id);

            return filteredSource;
        },
    },
    async mounted() {
        this.orders = await OrderRepository.getAll();
        this.orderStatuses = await OrderRepository.getStatuses();
    },
};
</script>
