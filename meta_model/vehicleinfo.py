import sys;
import os;
sys.path.append(os.path.dirname(os.path.abspath(os.path.dirname(__file__))))

from meta_model.common import *
from meta_model.imagerect import ImageRect

class Vehicle:
    license_image_buffer : str
    license_image_rect : ImageRect
    license_plate : list

    def __init__(self, license_image_buffer: str, license_image_rect : ImageRect, license_plate : list) -> None:
        self.license_image_buffer = license_image_buffer
        self.license_image_rect = license_image_rect
        self.license_plate = license_plate

    @staticmethod
    def from_dict(obj: Any):
        assert isinstance(obj, dict)
        license_image_buffer = from_str(obj.get("LicenseImageBuffer"))
        license_image_rect = ImageRect.from_dict(obj.get("LicenseImageRect"))
        license_plate = from_list(lambda x: int(x), obj.get("LicensePlate"))
        return Vehicle(license_image_buffer, license_image_rect, license_plate)

    def to_dict(self) -> dict:
        result: dict = {}
        result["LicenseImageBuffer"] = from_str(self.license_image_buffer)
        result["LicenseImageRect"] = to_class(ImageRect, self.license_image_rect)
        result["LicensePlate"] = from_list(self.license_plate)
        return result
