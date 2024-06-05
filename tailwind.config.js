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
        'ui': ['"Roboto"', ...defaultTheme.fontFamily.sans],
      },
      colors: {
        primary: '#7C58D3',
        secondary: '#221956',
        terciary: '#227C9D',
        ui: {
          light: {
            section: '#FFFFFF',
            page: '#F3F3F1',
            contrast: '#DDE1E6',
            hover: '#F2F8F5'
          }
        }
      },
    },
  },
  plugins: [
    require('@tailwindcss/forms'),
  ],
  // safelist: [{ pattern: /./}]
}

