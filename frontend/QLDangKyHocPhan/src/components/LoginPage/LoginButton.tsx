interface Props {
  onClick: () => void;
  disabled?: boolean;
}

export const LoginButton = ({ onClick, disabled }: Props) => (
  <button
    onClick={onClick}
    disabled={disabled}
    className="cursor-pointer bg-blue-600 text-white py-2 rounded hover:bg-blue-700 disabled:opacity-50 w-full"
  >
    Đăng nhập
  </button>
);
