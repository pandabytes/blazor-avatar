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

export function generateAvatar(avatarStyle: string, options: Options) {
  const style: any = styleMap.get(avatarStyle);
  if (!style) {
    throw new InvalidArgumentError(`Style "${avatarStyle}" not found.`);
  }
  return createAvatar(style, options).toString();
}

export function printOpts() {
  // const options = {
  //   ...schema.properties,
  //   ...micah.schema.properties,
  // };

  // console.log(getOptionsBySchema(bigSmile.schema));
  console.log(Object.entries(styles));
}

function getOptionsBySchema(schema: JSONSchema7) {
  const result: Record<string, any> = {};

  for (var key in schema.properties) {
    if (false === schema.properties.hasOwnProperty(key)) {
      continue;
    }

    const property = schema.properties[key];

    if (typeof property === 'object') {
      const option: Record<string, any> = {
        type: property.type,
      };

      if (option.type === 'integer') {
        option.type = 'number';
      }

      option.choices = [];

      if (property.enum) {
        option.choices.push(
          ...property.enum.filter((v) => typeof v === 'string')
        );
      }

      if (
        typeof property.items === 'object' &&
        'enum' in property.items &&
        property.items.enum
      ) {
        option.choices.push(
          ...property.items.enum.filter((v) => typeof v === 'string')
        );
      }

      if (option.choices.length === 0) {
        delete option.choices;
      }

      if (property.description) {
        option.description = property.description;
      }

      result[key] = option;
    }
  }

  return result;
}
