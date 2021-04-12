import Vue from 'vue';
import App from './App.vue';

Vue.config.productionTip = false;

new Vue({
    render: h => h(App),
    data: function() {
        return {
            car: ''
        };
    },
    created: function () {
        let urlParams = new URLSearchParams(window.location.search);
        (this as any).car = urlParams.get('car') ? urlParams.get('car') : '';

        console.log(urlParams, urlParams.get('car'));
        console.log(this);
    }
}).$mount('#app');
    