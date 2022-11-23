<template>
    <header>
        <nav id="topNav">
            <HeaderNavigationMenu />
            <SearchBox />
            <CartMenu />
        </nav>
    </header>
</template>

<style>
.sticky-header {
    position: fixed;
    left: 0;
    top: 0;
    width: 100vw;
    z-index: 999;
}
</style>

<script>
export default {
    async mounted() {
        //Sticky header
        /*const navItem = this.$el.querySelector("#topNav");
        onscroll = function myFunction() {
            if (pageYOffset > 62) {
                navItem.classList.add("sticky-header");
            } else {
                navItem.classList.remove("sticky-header");
            }
        };*/
    },
};
</script>
