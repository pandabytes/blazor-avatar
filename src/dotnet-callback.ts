
declare global {
  const DotNet: {
    attachReviver(callBack: (key: string, value: any) => Function | any): void;
  }
}

interface DotNetObjectReference {
  invokeMethod(methodName: string, ...args: any[]): any;
  invokeMethodAsync(methodName: string, ...args: any[]): Promise<any>;
}

type CallbackInterop = {
  isAsync: boolean,
  isCallBackInterop: boolean,
  dotNetRef: DotNetObjectReference,
}

DotNet.attachReviver((key, value) => {
  function isCallbackInterop(obj: any): obj is CallbackInterop {
    const isObjecType = obj && typeof obj === 'object';
    if (!isObjecType) {
      return false;
    }

    return obj.hasOwnProperty('isCallbackInterop') && obj.hasOwnProperty('dotNetRef');
  }

  if (isCallbackInterop(value)) {
    const dotNetRef = value.dotNetRef;

    if (value.isAsync) {
      // This callback will return a Promise
      return function() {
        return dotNetRef.invokeMethodAsync('Invoke', ...arguments);
      };
    }

    return function() {
      return dotNetRef.invokeMethod('Invoke', ...arguments);
    }
  }

  return value;
});

export {};
