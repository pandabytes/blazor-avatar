import { minidenticon } from './minidenticons.min.js'

export function minidenticonWrapper(
  seed: string,
  saturation?: number,
  lightness?: number,
  hashFn?: (str: string) => number
): string {
  return minidenticon(seed, saturation, lightness, hashFn);
}
