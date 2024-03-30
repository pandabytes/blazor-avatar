module.exports = {
  plugins: [
    ['@snowpack/plugin-optimize']
  ],

  buildOptions: {
    out: './wwwroot/js/',
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
    '**/*.csproj',
    '**/package.json',
    '**/package-lock.json',
    '**/snowpack.config.js',
    '**/tsconfig.json',
  ],
};
