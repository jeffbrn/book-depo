import VueCompositionApi from '@vue/composition-api';
import Vue from 'vue';

import App from './App.vue';
import router from './router';
import filters from './common/filters/formatting';

import './assets/tailwind.css';

Vue.config.productionTip = false;

// Vue Doco
// https://vuejs.org/

// Composition API Doco
// https://composition-api.vuejs.org/
// https://composition-api.vuejs.org/api.html
// https://github.com/vuejs/composition-api
Vue.use(VueCompositionApi);
Vue.filter('dateFormatter', filters().dateFormatter);

// Tailwind css Doco
// https://tailwindcss.com/
new Vue({
  router,
  render: (h) => h(App),
}).$mount('#app');
