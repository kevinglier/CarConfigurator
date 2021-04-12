<template>
    <div>
        {{ msg }}
        HHHH
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component
    export default class CarConfigurator extends Vue {
        @Prop() private msg!: string;

        constructor() {
            super();
            alert('!!');
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
