<template>
    <div class="dropdown show">
        <button
            type="button"
            data-bs-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
            :id="'dropdownMenuButton' + orderId"
            :class="
                'dropdown-toggle btn btn-sm w-100 btn-' +
                icons[activeStatus || orderStatus].css
            "
        >
            {{ icons[activeStatus || orderStatus].icon }}
            {{ activeStatus || orderStatus }}
        </button>
        <div
            class="dropdown-menu dropdown"
            :aria-labelledby="'dropdownMenuButton' + orderId"
        >
            <a
                :key="orderId + '_' + s.id"
                v-for="s in orderStatusesSource"
                href="#"
                @click.prevent="toggleOrderStatus(s.name)"
                class="dropdown-item"
                :class="{ disabled: s.name == orderStatus }"
            >
                {{ s.name }}
            </a>
        </div>
    </div>
</template>

<script>
import OrderRepository from "@/services/OrderRepository.ts";

export default {
    props: ["orderId", "orderStatus", "orderStatusesSource"],
    data() {
        return {
            activeStatus: "",
            icons: {
                Создан: {
                    icon: "🔄",
                    css: "primary",
                },
                "Комплектация заказа": {
                    icon: "📦",
                    css: "info",
                },
                "В доставке": {
                    icon: "🚙",
                    css: "warning",
                },
                Выполнен: {
                    icon: "✔️",
                    css: "success",
                },
            },
        };
    },
    methods: {
        async toggleOrderStatus(newStatus) {
            this.$el
                .querySelector(".order-status-cell div.show")
                .classList.remove("show");
            this.activeStatus = newStatus;
            await OrderRepository.changeStatus(this.orderId, newStatus);
        },
    },
};
</script>
