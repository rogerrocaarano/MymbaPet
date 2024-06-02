/** @type {import('tailwindcss').Config} */
const defaultTheme = require('tailwindcss/defaultTheme')

module.exports = {
  content: [
    "Views/**/*.cshtml",
    "Areas/**/*.cshtml",
  ],
  theme: {
    extend: {
      fontFamily: {
        'sans': ['"Hind"', ...defaultTheme.fontFamily.sans],
        'sans-header': ['"Poppins"', ...defaultTheme.fontFamily.sans],
        'brand': ['"Boogaloo"', 'cursive'],
      },
      colors: {
        primary: '#FF3850',
      }
    },
  },
  plugins: [
    require('@tailwindcss/forms'),
  ],
  // safelist: [{ pattern: /./}]
}

