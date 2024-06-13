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
                primary: {
                    100: "#f4f0ff",
                    200: "#D1D0FB",
                    300: "#AF97EB",
                    DEFAULT: "#986DF3",
                    500: "#7C58D3",
                    800: "#221956"
                },
                tertiary:{
                    100: "#F2F4F8",
                    DEFAULT: "#227C9D"
                },
                secondary:{
                    100: "#FFFFFF",
                    200: "#FEF7FF",
                    300: "#F6F6FE",
                    400: "#E7ECF3",
                    500: "#C1C7CD",
                    DEFAULT: "#E7B387",
                    700: "#EAAC8E1A"
                },
                alert: {
                    DEFAULT: "#E34D56",
                }

            },
        },
    },
    plugins: [
        require('@tailwindcss/forms'),
    ],
    // safelist: [{ pattern: /./}]
}

