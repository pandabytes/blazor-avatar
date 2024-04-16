import multiInput from 'rollup-plugin-multi-input';
import terser from '@rollup/plugin-terser';
import nodeResolve from '@rollup/plugin-node-resolve';

export default {
  input: ['./wwwroot/js/**/*.js'],
  output: {
    format: 'esm',
    dir: '.',
  },
  plugins: [ multiInput.default(), terser(), nodeResolve() ],
};
