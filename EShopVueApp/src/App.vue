<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
</script>

<template>
    <div class="wrapper">
        <HeaderMenu />

        <div id="content">
            <RouterView :key="$route.fullPath" />
        </div>
    </div>
</template>
