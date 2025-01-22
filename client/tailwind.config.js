module.exports = {
    content: [
        './src/**/*.{js,ts,jsx,tsx}',
    ],
    theme: {
        extend: {
            borderRadius: {
                lg: 'var(--radius)',
                md: 'calc(var(--radius) - 2px)',
                sm: 'calc(var(--radius) - 4px)'
            },
            colors: {
                background: 'hsl(var(--background))',
                foreground: 'hsl(var(--foreground))',
                card: {
                    DEFAULT: 'hsl(var(--card))',
                    foreground: 'hsl(var(--card-foreground))'
                },
                popover: {
                    DEFAULT: 'hsl(var(--popover))',
                    foreground: 'hsl(var(--popover-foreground))'
                },
                primary: {
                    DEFAULT: 'hsl(var(--primary))',
                    foreground: 'hsl(var(--primary-foreground))'
                },
                secondary: {
                    DEFAULT: 'hsl(var(--secondary))',
                    foreground: 'hsl(var(--secondary-foreground))'
                },
                muted: {
                    DEFAULT: 'hsl(var(--muted))',
                    foreground: 'hsl(var(--muted-foreground))'
                },
                accent: {
                    DEFAULT: 'hsl(var(--accent))',
                    foreground: 'hsl(var(--accent-foreground))'
                },
                destructive: {
                    DEFAULT: 'hsl(var(--destructive))',
                    foreground: 'hsl(var(--destructive-foreground))'
                },
                border: 'hsl(var(--border))',
                input: 'hsl(var(--input))',
                ring: 'hsl(var(--ring))',
                chart: {
                    '1': 'hsl(var(--chart-1))',
                    '2': 'hsl(var(--chart-2))',
                    '3': 'hsl(var(--chart-3))',
                    '4': 'hsl(var(--chart-4))',
                    '5': 'hsl(var(--chart-5))'
                },
                softBeige: 'hsl(var(--soft-beige))',
                yellow: {
                    150: '#fff4c2',
                },
                pink: {
                    150: '#ffdde0',
                },
                blue: {
                    150: '#d6e8ff',
                },
                indigo: {
                    150: '#e2dfff',
                },
                green: {
                    150: '#d8f5d2',
                },
                teal: {
                    150: '#d9f6f0',
                },
                cyan: {
                    150: '#d7f4fb',
                },
                purple: {
                    150: '#e8d7fa',
                },
                orange: {
                    150: '#ffe7cb',
                },
                red: {
                    150: '#ffd6d6',
                },
                lime: {
                    150: '#e4f8d8',
                },
            },
            keyframes: {
                'accordion-down': {
                    from: {
                        height: '0'
                    },
                    to: {
                        height: 'var(--radix-accordion-content-height)'
                    }
                },
                'accordion-up': {
                    from: {
                        height: 'var(--radix-accordion-content-height)'
                    },
                    to: {
                        height: '0'
                    }
                }
            },
            animation: {
                'accordion-down': 'accordion-down 0.2s ease-out',
                'accordion-up': 'accordion-up 0.2s ease-out'
            },
            screens: {
                'grid-md': '65rem',
                'grid-lg': '76rem',
            },
            boxShadow: {
                softGlow: "0 0 20px 20px hsla(30, 50%, 97%, 0.95)",
            },
            strokeWidth: {
                '5': '5px',
            }
        }
    },
    plugins: [
        require("tailwindcss-animate"),
        require("tailwind-scrollbar-hide"),
    ],
};