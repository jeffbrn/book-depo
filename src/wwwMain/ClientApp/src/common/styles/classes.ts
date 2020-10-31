const buttonStyles = ['text-sm', 'font-bold', 'py-2', 'px-4', 'rounded'];

const primaryButton = [...buttonStyles, 'bg-blue-500', 'hover:bg-blue-700', 'text-white'];
const secondaryButton = [...buttonStyles, 'bg-gray-600', 'hover:bg-gray-700', 'text-white'];

const labelStyle = ['text-xs', 'text-blue-400', 'mt-6'];
const inputStyle = ['w-full', 'border', 'rounded', 'py-2', 'px-4', 'text-sm', 'bg-gray-100', 'text-gray-700', 'border-gray-300'];
const inputNumStyle = ['w-32', 'border', 'rounded', 'py-2', 'px-4', 'text-sm', 'bg-gray-100', 'text-gray-700', 'border-gray-300'];

const DefaultStyles = {
  primaryButton,
  secondaryButton,
  labelStyle,
  inputStyle,
  inputNumStyle,
};

export default DefaultStyles;
