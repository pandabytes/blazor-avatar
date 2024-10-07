import { createAvatar, Options } from '@dicebear/core';
import * as styles from '@dicebear/collection';

class InvalidArgumentError extends Error {
  constructor(message: string) {
    super(message);
    this.name = 'InvalidArgumentError';
  }
}

const styleMap = new Map(Object.entries(styles));

/**
 * Generate the avatar and return the data URI.
 * @param avatarStyle The avatar style to be used in the generation.
 * @param options Options to generate the avatar.
 * @returns Data URI the generated avatar.
 */
export function generateAvatar(avatarStyle: string, options: Options): string {
  const style = styleMap.get(avatarStyle) as any;
  if (!style) {
    throw new InvalidArgumentError(`Style "${avatarStyle}" not found.`);
  }

  // Have to use data uri instead of svg.
  // Not sure why svg doesn't work if
  // we load a style and then loading another style
  // would not render it on the browser, although
  // the generated svg looks correct
  return createAvatar(style, options).toDataUri();
}
