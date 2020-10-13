/* eslint global-require: "off" */
module.exports = {
  future: {
    removeDeprecatedGapUtilities: true,
    purgeLayersByDefault: true,
  },
  purge: [
    './src/**/*.html',
    './src/**/*.vue',
  ],
  theme: {
    height: {
      test: '70vh',
    },
    extend: {
      colors: {
        'smoke-60': 'rgba(0, 0, 0, 0.60)',
      },
    },
  },
  variants: {},
  plugins: [
  ],
};
