import { createAvatar, Options  } from '@dicebear/core';
import { JSONSchema7 } from 'json-schema';
import * as styles from '@dicebear/collection';

class InvalidArgumentError extends Error {
  constructor(message: string) {
    super(message);
    this.name = 'InvalidArgumentError';
  }
}

const styleMap = new Map(Object.entries(styles));

/**
 * Generate the avatar and return its string representation.
 * @param avatarStyle The avatar style to be used in the generation.
 * @param options Options to generate the avatar.
 * @returns String representation of the generated avatar.
 */
export function generateAvatar(avatarStyle: string, options: Options) {
  const style: any = styleMap.get(avatarStyle);
  if (!style) {
    throw new InvalidArgumentError(`Style "${avatarStyle}" not found.`);
  }
  return createAvatar(style, options).toString();
}
