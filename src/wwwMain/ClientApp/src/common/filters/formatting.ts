import dayjs from 'dayjs';

// Day.js Doco
// https://day.js.org/en/

export default function () {
  function dateFormatter(value: Date | null): string {
    if (value === null) return '';
    return dayjs(value).format('DD-MMM-YYYY');
  }

  return {
    dateFormatter,
  };
}
