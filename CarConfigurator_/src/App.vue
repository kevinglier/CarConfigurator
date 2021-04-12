<template>
    <div id="app">
        <CarConfigurator :msg="car" />
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import CarConfigurator from './components/CarConfigurator.vue';

    @Component({
        components: {
            CarConfigurator
        }
    })
    export default class App extends Vue {
        car: string|null = null;
    }
</script>

<style>
</style>
