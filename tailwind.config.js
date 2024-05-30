/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "Views/**/*.cshtml",
    "Areas/**/*.cshtml",
  ],
  theme: {
    extend: {
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

