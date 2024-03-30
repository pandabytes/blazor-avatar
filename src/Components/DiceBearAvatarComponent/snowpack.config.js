module.exports = {
  plugins: [
    ['@snowpack/plugin-optimize']
  ],

  buildOptions: {
    out: '../../wwwroot/js/Components/DiceBearAvatarComponent',
    clean: true
  },
  mount: {
    '.': '/'
  },
  include: [
    '**/*.ts'
  ],
  exclude: [
    '**/node_modules/**/*',
    '**/*.razor',
    '**/*.cs',
  ]
};
