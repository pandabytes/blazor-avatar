/**
 * See https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-dotnet-from-javascript?view=aspnetcore-7.0#create-javascript-object-and-data-references-to-pass-to-net
 */
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

/**
 * Taken from https://remibou.github.io/How-to-send-callback-to-JS-Interop-in-Blazor/
 * Essentially this converts any C# delegate (Func, Action, & EventCallback) to
 * a JS function that can be invoked by JS code.
 */
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
