import VueCompositionApi from '@vue/composition-api';
import Vue from 'vue';

import { library } from '@fortawesome/fontawesome-svg-core';
import { faAngleDoubleRight, faBinoculars, faPencilAlt } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import App from './App.vue';
import router from './router';
import filters from './common/filters/formatting';

import './assets/tailwind.css';

// FontAwesome Doco
// https://fontawesome.com/
// https://www.npmjs.com/package/@fortawesome/vue-fontawesome#get-started
// https://fontawesome.com/cheatsheet
library.add(faAngleDoubleRight, faBinoculars, faPencilAlt);

Vue.component('font-awesome-icon', FontAwesomeIcon);

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
