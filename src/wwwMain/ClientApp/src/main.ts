import VueCompositionApi from '@vue/composition-api';
import Vue from 'vue';

import App from './App.vue';
import router from './router';
import filters from './common/filters/formatting';

import './assets/tailwind.css';

Vue.config.productionTip = false;

Vue.use(VueCompositionApi);
Vue.filter('dateFormatter', filters().dateFormatter);

new Vue({
  router,
  render: (h) => h(App),
}).$mount('#app');
