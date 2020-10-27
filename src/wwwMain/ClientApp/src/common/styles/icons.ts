import Vue from 'vue';

import { library } from '@fortawesome/fontawesome-svg-core';
import {
  faAngleDoubleRight, faBinoculars, faPencilAlt, faTimesCircle, faCheckCircle,
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

// FontAwesome Doco
// https://fontawesome.com/
// https://www.npmjs.com/package/@fortawesome/vue-fontawesome#get-started
// https://fontawesome.com/cheatsheet
library.add(faAngleDoubleRight, faBinoculars, faPencilAlt, faTimesCircle, faCheckCircle);

Vue.component('font-awesome-icon', FontAwesomeIcon);
