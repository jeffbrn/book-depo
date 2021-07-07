module.exports = {
  purge: { content: ['./public/**/*.html', './src/**/*.vue'] },
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      width: {
        '1/7': '14.2857143%',
        '1/8': '12.5%'
      }
    },
  },
  variants: {
    extend: {},
  },
  plugins: [],
}
