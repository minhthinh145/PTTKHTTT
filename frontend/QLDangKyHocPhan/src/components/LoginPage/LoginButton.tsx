interface Props {
  onClick: () => void;
  disabled?: boolean;
}

export const LoginButton = ({ onClick, disabled }: Props) => (
  <button
    onClick={onClick}
    disabled={disabled}
    className="w-full bg-indigo-600 text-white py-3 rounded-full
                       font-medium hover:bg-indigo-700 focus:outline-none focus:ring-2 
                       focus:ring-offset-2 focus:ring-indigo-500 transition-colors hover-scale"
  >
    Đăng nhập
  </button>
);
