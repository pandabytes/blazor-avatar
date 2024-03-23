
/**
 * Adapt from https://codepen.io/arturheinze/pen/ZGvOMw
 * Asume parameters have been validated by C# code.
 * @param name 
 * @param size 
 * @returns 
 */
export function getAvatarUrl(
  firstName: string,
  lastName: string,
  size: number,
  color?: string | undefined
): string {
  const colors = [
    "#1abc9c",
    "#2ecc71",
    "#3498db",
    "#9b59b6",
    "#34495e",
    "#16a085",
    "#27ae60",
    "#2980b9",
    "#8e44ad",
    "#2c3e50",
    "#f1c40f",
    "#e67e22",
    "#e74c3c",
    "#95a5a6",
    "#f39c12",
    "#d35400",
    "#c0392b",
    "#bdc3c7",
    "#7f8c8d"
  ];

  const initials = firstName.charAt(0).toUpperCase() +
                   lastName.charAt(0).toUpperCase();
  const colorIndex = initials.charCodeAt(0) % colors.length;
  color = color || colors[colorIndex];

  const canvas = document.createElement('canvas');
  canvas.width = size;
  canvas.height = size;

  const context = canvas.getContext("2d");
  context.fillStyle = color;
  context.fillRect(0, 0, size, size);
  context.font = `${Math.round(size / 2)}px Arial`;
  context.textAlign = "center";
  context.fillStyle = "#FFF";
  context.fillText(initials, size / 2, size / 1.5);

  const dataUrl = canvas.toDataURL();
  canvas.remove();
  return dataUrl;
}

export function isValidColor(colorStr: string): boolean {
  return CSS.supports('color', colorStr);
}
