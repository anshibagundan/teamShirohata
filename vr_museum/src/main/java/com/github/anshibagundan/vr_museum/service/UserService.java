package com.github.anshibagundan.vr_museum.service;

import com.github.anshibagundan.vr_museum.dto.UserDto;

import java.util.List;

public interface UserService {
    UserDto createUser(UserDto userDto);

    UserDto getUserById(Long userId);

    List<UserDto> getAllUser();

    UserDto updateUser(Long userId, UserDto updatedUser);

    void deleteUser(Long userId);
}
