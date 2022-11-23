<template>
    <div v-if="isOpened" class="order-modal-success">
        <div class="order-model-success__inner">
            <h2>
                <strong style="color: orange">{{ customerName }}</strong
                >, спасибо за заказ!
            </h2>
            <button class="btn btn-lg btn-primary mb-4" @click="isOpened = false">
                Закрыть
            </button>
            <p>
                Заказ с номером
                <strong style="color: orange">{{ orderId }}</strong> принят в работу
            </p>
        </div>
    </div>
</template>

<style>
.order-modal-success {
    height: 100vh;
    display: flex;
    justify-content: center;
    align-items: center;
    position: fixed;
    z-index: 100000;

    background: #00807ade;
    max-width: 100%;
    margin: 0;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
}
.order-model-success__inner {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;

    width: 50%;
    height: 75%;
    border: 1px solid #4f94ab;
    border-radius: 16px;
    background-color: #0093e9;
    background-image: linear-gradient(160deg, #0093e9 0%, #80d0c7 100%);

    /* background-image: url("/src/assets/images/success-order-background2.jpg"); */
}
</style>

<script>
export default {
    props: ["customerName", "orderId"],
    data() {
        return {
            isOpened: true,
        };
    },
};
</script>
