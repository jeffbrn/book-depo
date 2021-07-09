import { createApp } from 'vue'
import App from './App.vue'
import router from './services/router'
import store from './services/store'
import filters from './common/mixins/formatting';

import './assets/styles/main.css'

const app = createApp(App).use(store).use(router);
filters().RegisterFilters(app.config);
app.mount('#app');
