import * as moment from 'moment';

export default function() {
  function dateFormatter1(value) {
    if (typeof value === 'undefined') return '';
    if (value === null) return '';
    return moment(value).format("Do-MMM-YYYY");
  }

  function datetimeFormatter1(value) {
    if (typeof value === 'undefined') return '';
    if (value === null) return '';
    return moment(value).format("Do-MMM-YYYY h:mm a");
  }

  function RegisterFilters(appConfig) {
    appConfig.globalProperties.$filters = {
      dateFormatter1,
      datetimeFormatter1
    };
  }

  return {
    RegisterFilters
  };
}